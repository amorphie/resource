#!/bin/sh

# Let's wait for the Vault server to start.
sleep 5

# Check your secret endpoint
SECRET_CHECK=$(curl -s -o /dev/null -w "%{http_code}" -X GET 'http://vault:8200/v1/secret/data/amorphie-secretstore' -H "X-Vault-Token: admin")

# If there is no secret, create it and set the relevant keys.
if [ "$SECRET_CHECK" -ne 200 ]; then
  curl -X POST 'http://vault:8200/v1/secret/data/amorphie-secretstore' \
  -H "Content-Type: application/json" \
  -H "X-Vault-Token: admin" \
  -d '{
    "data": {
              "ElasticApm:Environment": "Dev",
              "ElasticApm:SecretToken": "",
              "ElasticApm:ServerUrl": "http://localhost:5200",
              "ElasticApm:ServiceName": "amorphie-resource",
              "ElasticApm:SpanFramesMinDuration": "0",
              "ElasticApm:TransactionIgnoreUrls": "/healthz,/swagger/*,/index.html\",/dapr/*",
              "Logging:LogResponse": "true",
              "Logging:SanitizeFieldNames": "access_token,refresh_token,client_secret,authorization",
              "Logging:SanitizeHeaderNames": "authorization,authentication,client_secret,x-userinfo",
              "PostgreSql": "Host=localhost:5432;Database=resources;Username=postgres;Password=postgres",
              "Serilog:MinimumLevel:Default": "Information",
              "Serilog:MinimumLevel:Override:amorphie.resource": "Information",
              "Serilog:WriteTo:0:Args:formatter": "Serilog.Formatting.Compact.CompactJsonFormatter, Serilog.Formatting.Compact",
              "Serilog:WriteTo:0:Args:path": "logs/log-amorphie-resource.json",
              "Serilog:WriteTo:0:Args:rollingInterval": "Day",
              "Serilog:WriteTo:0:Name": "File",
              "Serilog:WriteTo:1:Name": "Console"
            }
  }'
else
  echo "Secret 'myprojectname-secret' already exists."
fi
