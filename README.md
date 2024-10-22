


# ECommerce Project

This project aims to [briefly describe the project purpose].

## Technologies Used

- ASP.NET Core
- CQRS and Mediator Design Utilization
- Onion Architecture Implementation
- Swagger
- PostgreSQL
- Docker (optional, if used)
- JWT Authentication (optional, if used)
- Authentication with JWT Token and Rerfresh Token
- Login with Google and Facebook
- Files are uploaded to Microsoft Azure storage

## Project Overview

This project [provide a more detailed description of the project's goals and scope].

## Folder Structure

The project follows the principles of Onion Architecture, with the following folder structure:

- **Application**: Application services and CQRS commands/handlers.
- **Domain**: Domain models and business logic.
- **Infrastructure**: Database connections, external services, and data persistence layer.
- **Presentation**: API controllers and Swagger configuration.

## Persistence Layer

The persistence layer includes database interactions and concrete service implementations:

- **ProductRepository**: Handles database operations related to products.
- **OrderRepository**: Manages database operations for orders.
- **UserRepository**: Implements database operations for user-related data.

## Product

Create Product: Endpoint to create a new product.

Update Product: Endpoint to update an existing product.

Search Product: Endpoint to search for products based on a search term.

Create Product Image: Endpoint to upload and associate images with a product.

Delete Product Image: Endpoint to delete an image associated with a product.

Get Product by Id: Endpoint to retrieve details of a product by its unique identifier.

Change Showcase Image for Product: Endpoint to set a specific image as the showcase image for a product.

## Basket

Add Product to User Basket: Endpoint to add a product to a user's basket.

Update Product in User Basket: Endpoint to update a product quantity in the user's basket.

Complete User Basket: Endpoint to finalize the selection and prepare for order.

## Order 

User Order: Endpoint for a user to place an order.

Admin Complete Order: Endpoint for an admin to mark an order as completed.

Admin Delete Order: Endpoint for an admin to delete an order.

## Login


User Login: Endpoint for users to log in using credentials.

Google Login: Endpoint for users to log in using Google authentication.

Facbook Login: Endpoint for users to log in using Facbook authentication.

Reset Password via Email: Endpoint to initiate a password reset process via email.

Reset JWT Token: Endpoint to regenerate a new JWT token after expiration.

## Register

User Register: Endpoint for user registration.
Google Register: Endpoint for user registration using Google authentication.
FabebookLogin :  Endpoint for user registration using Facebook authentication.


## Validation and Filters

### Validation Filters

Validation filters are implemented using FluentValidation for robust input validation:

- **CreateProductValidator**: Validates `CreateProductCommandRequest` before processing, enforcing rules for product name, description, stock, and price.

### Action Filters

Action filters, such as `ValidationFilter`, intercept requests before execution to ensure data validity:

- **ValidationFilter**: Checks `ModelState.IsValid` before action execution. If invalid, it returns a `BadRequestObjectResult` with detailed validation errors.

## Getting Started

To get started with the project locally or on a server, follow these steps:
1. [Instructions on setting up prerequisites, dependencies, and environment variables].
2. [How to run the project locally or deploy it on a server].
3. [Additional configuration steps, if any].

## API Documentation

Explore the project's API endpoints using Swagger UI at [URL]. Here you can test and interact with various endpoints.

1. Introduction
2. Project Structure
   - Onion Architecture Overview
   - Implementation of Onion Architecture with .NET Framework
   - Layers and Responsibilities
     - Application Layer
     - Domain Layer
     - Infrastructure Layer
       - Infrastructure
       - Persistence
     - Presentation Layer (API)
   - CQRS Design Overview
   - Mediator Design Implementation
3. Advantages of the Project
4. Example Usage Scenarios
   - Product Addition or Update
   - Viewing Product List
   - Creating an Order
5. Technical Details
   - .NET Framework 
   - Onion Architecture Implementation
   - CQRS and Mediator Design Utilization
   - Authentication with JWT Token and Rerfresh Token
   - Login with Google and Facebook
   - Files are uploaded to Microsoft Azure storage
6. Example Code Snippets
   - Command or Query Processing
   - Managing Commands and Queries using Mediator
7. Conclusions and Recommendations
   - Project Challenges and Solutions
   - Lessons Learned
   - Recommendations and Future Improvements
8. Sources
   - Sources of the technologies and designs used
   - Azure Blob Storage Service
   - Google Apis Auth



## API
![Product Management Screenshot](https://i.imgur.com/mdfNnYJ.png)

![Product Management Dashboard](https://i.imgur.com/ZXVLt7w.png)

![Order Management Dashboard](https://i.imgur.com/pgtRdB5.png)

![Eklenmiş Resim](https://i.imgur.com/wivCm9O.png)
