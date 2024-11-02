# Expense Tracking application

## Using `docker-compose`

```pwsh
# Set working directory
cd C:\Git\expense-tracking

# Start containers
docker-compose -f docker-compose.yaml up
```

## API

### Docker commands
```pwsh
# Set working directory
cd C:\Git\expense-tracking\src\api

# Build container image
docker build -t expense-tracking-api .

# Access the URL via browser
curl http://localhost:8081/
```

