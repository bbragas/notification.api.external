path :=$(if $(path), $(path), "./")
awsRegion:="us-east-1"

.PHONY: help
help: ## - Show this help message
	@printf "\033[32m\xE2\x9c\x93 usage: make [target]\n\n\033[0m"
	@grep -E '^[a-zA-Z_-]+:.*?## .*$$' $(MAKEFILE_LIST) | sort | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'

.PHONY: up
up: ## - start docker compose
	@ cd _docker && docker-compose -f docker-compose.yml up

kill: ## - kill docker compose
	@ cd _docker && docker-compose -f docker-compose.yml kill

hard-kill: ## - kill docker compose
	@ cd _docker && docker-compose -f docker-compose.yml kill
	@ docker rm -f grafana prometheus mongodb
	@ cd _docker && rm -rf grafana_data prometheus_data mongodb_data

.PHONY: build-common
build-common: ## - execute build common tasks
	@ dotnet clean Notification.Api.External.sln
	@ dotnet restore Notification.Api.External.sln

.PHONY: build
build: build-common ## - build a debug assembly to the current platform (windows, linux or darwin(mac))
	@ dotnet build --no-restore src/Notification.Api.External.Webhook/Notification.Api.External.Webhook.csproj
	@ ls -lah $(path)/src/Notification.Api.External.Webhook/bin/Debug/net6.0/

.PHONY: build-release
build-release: build-common ## - build a release assembly to the current platform (windows, linux or darwin(mac))
	@ dotnet build -c Release --no-restore src/Notification.Api.External.Webhook/Notification.Api.External.Webhook.csproj
	@ ls -lah $(path)/src/Notification.Api.External.Webhook/bin/Release/net6.0/

.PHONY: test
test: build-release ## - build a release assembly to the current platform (windows, linux or darwin(mac))
	@ ls tests/*Tests*/*.csproj | xargs -L1 dotnet test --logger:trx

scan-sec:
	@ dotnet tool update -g security-scan --version 5.6.3
	@ security-scan Notification.Api.External.sln --export=notification-ext-api.sarif
	@ echo ""
	@ @ cat $(path)/notification-ext-api.sarif | jq -r '(["LEVEL","LINE","FILE","RULE-ID","MESSAGE"] | (., map(length*"-"))), (.runs[].results[] | [.level, .locations[].physicalLocation.region.startLine, .locations[].physicalLocation.artifactLocation.uri, .ruleId, .message.text] ) | @tsv' | column -t 

.PHONY: lint
lint: ## - check lint against dotnet projects
	@ dotnet tool update -g dotnet-format
	@ dotnet format --no-restore --verbosity detailed

.PHONY: publish
publish: ## - build a release assembly to the current platform (windows, linux or darwin(mac))
	@ ls -lah
	@ rm -rf $(path)/src/Notification.Api.External.Webhook/publish
	@ dotnet publish -c Release $(path)src/Notification.Api.External.Webhook/Notification.Api.External.Webhook.csproj -o $(path)/src/Notification.Api.External.Webhook/publish
	@ rm -f $(path)/src/Notification.Api.External.Webhook/publish/*.pdb $(path)/src/Notification.Api.External.Webhook/publish/appsettings.Development.json
	@ ls -lah $(path)/src/Notification.Api.External.Webhook/publish

.PHONY: docker-build
docker-build: ## - build docker image
	@ docker build -f _docker/Dockerfile -t $(imageName):$(version) .

docker-push:
	@ docker tag "$(imageName)":"$(version)" 434885180636.dkr.ecr."$(awsRegion)".amazonaws.com/"$(imageName)":"$(version)"
	@ docker push 434885180636.dkr.ecr."$(awsRegion)".amazonaws.com/"$(imageName)":"$(version)"

.PHONY: docker-scan
docker-scan: ## - Scan for known vulnerabilities
	@ printf "\033[32m\xE2\x9c\x93 Scan for known vulnerabilities the smallest and secured golang docker image based on scratch\n\033[0m"
	@ docker scan -f _docker/Dockerfile notification-ext-api

.PHONY: sonar-scan
sonar-scan: ## - start sonar qube locally with docker (you will need docker installed in your machine)
	@ # login with user: admin pwd: notification-ext-api
	@ $(SHELL) _scripts/sonar-start.sh

.PHONY: sonar-stop
sonar-stop: ## - stop sonar qube docker container
	@ docker stop sonarqube
