Calculator API with JWT Authentication

This project is a simple Calculator API that performs arithmetic operations (such as addition, subtraction, multiplication, division) 
on two numerical values provided by the user. The API is built with JWT (JSON Web Token) authentication and is containerized using Docker.

Features:
JWT Authentication: The API requires a valid token for all operations. The token is issued through a login API and has a 20-minute expiration time.

Authorization: The API is protected by an authorization check using JWT Bearer tokens. Only requests with valid tokens will be accepted.

Post Request: The API uses the HTTP POST method to send the calculation request.

Swagger Integration: The API is defined via a YAML file and built with Swagger to provide documentation and ease of use.

Dockerized: The project is packaged within Docker containers, including the necessary configuration files (Dockerfile and docker-compose.yml) for building and running the application in a containerized environment.

How to Use
1. Login and Get Token
To authenticate and get a JWT token, send a POST request to /api/auth/login with the following JSON body:

Edit
{
  "username": "testuser",
  "password": "Pass1357!"
}
The response will include a token, which you can use to authorize further requests.
Important: The token expires after 20 minutes.

2.Headers:

Authorization: Bearer token obtained from the login process

X-Operation-Type: Operation type (e.g., "add", "subtract")

Run the dokcer in terminal : docker-compose up -d


Running the Project in Docker
1. Clone the Repository

git clone https://github.com/reutshasha/CalculatorApi.git

2. Build and Run with Docker
Build the Docker container:

docker build -t calculator-api .
docker-compose up

3. Access the Application
Once the container is running, you can access the API on http://localhost:8080.

Project Structure
CalculatorApi/: Main API project
BL/: Business Logic layer
DAL/: Data Access layer
Dockerfile: Docker build instructions
docker-compose.yml: Docker Compose configuration for running the application
