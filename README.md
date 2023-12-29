
# Introduction

Api developed to receive external requests such as Events Webhooks from partners.

# Getting Started

Add the following settings in the appSettings.json

```json
{
    "Mongo": {
        "DatabaseName": "",
        "ConnectionString": ""
    },
    "EventBusApiUrl": "http://dev.eventbus.api.vegait.com",
    "IpWhitelistSettings": {
        "SmsWhitelist": [],
        "EmailWhitelist": []
    }
}
```

# Events

This application produces following events:

<b> EmailBounceEvent </b>

```json
{
	"$schema": "https://json-schema.org/draft/2019-09/schema",
	"$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailBounceEvent.json",
	"type": "object",
	"title": "EmailBounceEvent",
	"description": "Whenever an email is rejected at the destination provider, reporting full mailbox, non-existent email etc.",
	"required": [
		"id",
		"contentType",
		"source",
		"version",
		"description",
		"type"
	],
	"properties": {
		"id": {
			"type": "string",
			"description": "A guid that represents the identifier of the EmailBounceEvent on the notification platform.",
			"examples": [
				"4c793fe0-7065-4555-b962-8ad2b5238ade"
			]
		},
		"version": {
			"type": "string",
			"description": "A text that describe the version of de EmailBounceEvent.",
			"examples": [
				"1.0"
			]
		},
		"source": {
			"type": "string",
			"description": "A text that represents the project where the EmailBounceEvent is located.",
			"examples": [
				"Notification.Api.External"
			]
		},
		"contentType": {
			"type": "string",
			"description": "A text that represents a mime type of the content.",
			"examples": [
				"application/json;base64"
			]
		},
		"type": {
			"type": "string",
			"description": "A text that describe the full name of the EmailBounceEvent on the Notification.Api.External.",
			"examples": [
				"Notification.Api.External.Application.Events.v1.EmailBounceEvent"
			]
		},
		"schema": {
			"type": "string",
			"default": "null",
			"description": "A URI which addressed a Json File that describe the EmailBounceEvent structure.",
			"examples": [
				"https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailBounceEvent.json"
			]
		},
		"description": {
			"type": "string",
			"description": "A text that describe the EmailBounceEvent.",
			"examples": [
				"The message has not been delivered"
			]
		}
	}
}
```

<b>EmailClickEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailClickEvent.json",
  "type": "object",
  "title": "EmailClickEvent",
  "description": "Whenever a url is clicked in an email.",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the EmailClickEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de EmailClickEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the EmailClickEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the EmailClickEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.EmailClickEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the EmailClickEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailClickEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the EmailClickEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```

<b>EmailReadEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailReadEvent.json",
  "type": "object",
  "title": "EmailReadEvent",
  "description": "Whenever a recipient confirms the reading of a sent email (viewed images contained in the message).",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the EmailReadEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de EmailReadEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the EmailReadEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the EmailReadEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.EmailReadEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the EmailReadEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailReadEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the EmailReadEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```

<b>EmailUnsubscribeEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailUnsubscribeEvent.json",
  "type": "object",
  "title": "EmailUnsubscribeEvent",
  "description": "Whenever a recipient requests not to receive your emails anymore.",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the EmailUnsubscribeEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de EmailUnsubscribeEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the EmailUnsubscribeEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the EmailUnsubscribeEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.EmailUnsubscribeEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the EmailUnsubscribeEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/EmailUnsubscribeEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the EmailUnsubscribeEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```

<b>SmsDeliveredEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsDeliveredEvent.json",
  "type": "object",
  "title": "SmsDeliveredEvent",
  "description": "The message has been successfully processed and delivered.",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the SmsDeliveredEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de SmsDeliveredEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the SmsDeliveredEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the SmsDeliveredEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.SmsDeliveredEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the SmsDeliveredEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsDeliveredEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the SmsDeliveredEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```

<b>SmsExpiredEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsExpiredEvent.json",
  "type": "object",
  "title": "SmsExpiredEvent",
  "description": "The message has been sent and has either expired due to pending past its validity period (our platform default is 48 hours), or the delivery report from the operator has reverted the expired as a final status.",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the SmsExpiredEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de SmsExpiredEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the SmsExpiredEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the SmsExpiredEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.SmsExpiredEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the SmsExpiredEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsExpiredEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the SmsExpiredEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```

<b>SmsPendingEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsPendingEvent.json",
  "type": "object",
  "title": "SmsPendingEvent",
  "description": "The message has not been delivered.",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the SmsPendingEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de SmsPendingEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the SmsPendingEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the SmsPendingEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.SmsPendingEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the SmsPendingEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsPendingEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the SmsPendingEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```

<b>SmsRejectedEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsRejectedEvent.json",
  "type": "object",
  "title": "SmsRejectedEvent",
  "description": "The message has not been delivered.",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the SmsRejectedEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de SmsRejectedEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the SmsRejectedEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the SmsRejectedEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.SmsRejectedEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the SmsRejectedEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsRejectedEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the SmsRejectedEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```

<b>SmsUndeliverableEvent</b>

```json
{
  "$schema": "https://json-schema.org/draft/2019-09/schema",
  "$id": "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsUndeliverableEvent.json",
  "type": "object",
  "title": "SmsUndeliverableEvent",
  "description": "The message has not been delivered.",
  "required": [
    "id",
    "contentType",
    "source",
    "version",
    "description",
    "type"
  ],
  "properties": {
    "id": {
      "type": "string",
      "description": "A guid that represents the identifier of the SmsUndeliverableEvent on the notification platform.",
      "examples": [
        "4c793fe0-7065-4555-b962-8ad2b5238ade"
      ]
    },
    "version": {
      "type": "string",
      "description": "A text that describe the version of de SmsUndeliverableEvent.",
      "examples": [
        "1.0"
      ]
    },
    "source": {
      "type": "string",
      "description": "A text that represents the project where the SmsUndeliverableEvent is located.",
      "examples": [
        "Notification.Api.External"
      ]
    },
    "contentType": {
      "type": "string",
      "description": "A text that represents a mime type of the content.",
      "examples": [
        "application/json;base64"
      ]
    },
    "type": {
      "type": "string",
      "description": "A text that describe the full name of the SmsUndeliverableEvent on the Notification.Api.External.",
      "examples": [
        "Notification.Api.External.Application.Events.v1.SmsUndeliverableEvent"
      ]
    },
    "schema": {
      "type": "string",
      "default": "null",
      "description": "A URI which addressed a Json File that describe the SmsUndeliverableEvent structure.",
      "examples": [
        "https://vegait.visualstudio.com/Notification/_git/notification.api.external?path=/schemas/SmsUndeliverableEvent.json"
      ]
    },
    "description": {
      "type": "string",
      "description": "A text that describe the SmsUndeliverableEvent.",
      "examples": [
        "The message has not been delivered"
      ]
    }
  }
}
```