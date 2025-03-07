# E-Commerce APIs

## Description

This is a RESTful API for an E-Commerce application built with ASP.NET Core. The API provides functionalities for managing products, categories, user authentication, and cart operations.

## Base URL

```
https://your-api-domain.com/api
```

---

## Endpoints

### **Products**

#### 1. Get All Products

- **Endpoint:** `GET /api/Products/All`
- **Description:** Retrieves all available products.
- **Responses:**
  - `200 OK`: Returns a list of products.
  - `404 Not Found`: No products found.

#### 2. Add a New Product

- **Endpoint:** `POST /api/Products/Add`
- **Consumes:** `multipart/form-data`
- **Description:** Adds a new product.
- **Request Body:** `NewProductDTO`
- **Responses:**
  - `200 OK`: Product added successfully.
  - `400 Bad Request`: Invalid input data.
  - `500 Internal Server Error`: Error while adding product.

#### 3. Get Products by Category

- **Endpoint:** `GET /api/Products/Category/{id}`
- **Description:** Retrieves products by category ID.
- **Responses:**
  - `200 OK`: Returns the products of the given category.
  - `404 Not Found`: No products found for the category.

#### 4. Explore Products

- **Endpoint:** `GET /api/Products/ExploreProducts`
- **Description:** Retrieves a curated list of products for exploration.
- **Responses:**
  - `200 OK`: Returns the list of explored products.
  - `404 Not Found`: No products available.

---

### **Users**

#### 1. Register a New User

- **Endpoint:** `POST /api/Account/Register`
- **Description:** Registers a new user.
- **Request Body:** `RegisterDTO`
- **Responses:**
  - `200 OK`: Registration successful, check email for confirmation.
  - `400 Bad Request`: Validation errors or email already in use.

#### 2. User Login

- **Endpoint:** `POST /api/Account/Login`
- **Description:** Authenticates a user and returns a JWT token.
- **Request Body:** `LoginDTO`
- **Responses:**
  - `200 OK`: Returns a JWT token.
  - `400 Bad Request`: Invalid credentials or unconfirmed email.

#### 3. Confirm Email

- **Endpoint:** `GET /api/Account/ConfirmEmail?userEmail={email}&Token={token}`
- **Description:** Confirms a user’s email address.
- **Responses:**
  - `200 OK`: Email confirmed successfully.
  - `400 Bad Request`: Invalid token or user not found.

#### 4. Resend Confirmation Email

- **Endpoint:** `GET /api/Account/ReConfirmEmail?Email={email}`
- **Description:** Resends the email confirmation link.
- **Responses:**
  - `200 OK`: Email resent successfully.
  - `400 Bad Request`: User already confirmed.

#### 5. Get User Information

- **Endpoint:** `GET /api/Account/UserInfo`
- **Authorization:** Requires authentication.
- **Description:** Retrieves user details.
- **Responses:**
  - `200 OK`: Returns user details.
  - `401 Unauthorized`: User not authenticated.
  - `404 Not Found`: User not found.

---

### **Cart**

#### 1. Add Product to Cart

- **Endpoint:** `POST /api/Cart/AddToCart`
- **Authorization:** Requires authentication.
- **Description:** Adds a product to the user’s cart.
- **Request Body:** `AddProductCartDTO`
- **Responses:**
  - `200 OK`: Product added successfully.
  - `400 Bad Request`: Invalid quantity or stock unavailable.
  - `404 Not Found`: Product not found.

---

### **Category**

#### 1. Get All Categories

- **Endpoint:** `GET /api/Categories/GetAllCategories`
- **Description:** Retrieves all product categories.
- **Responses:**
  - `200 OK`: Returns all categories.
  - `404 Not Found`: No categories found.

#### 2. Add a New Category (Admin Only)

- **Endpoint:** `POST /api/Categories/AddCat`
- **Authorization:** Requires `Admin` role.
- **Description:** Adds a new category.
- **Request Body:** `CategoryDTO`
- **Responses:**
  - `200 OK`: Category added successfully.
  - `500 Internal Server Error`: Could not save category.

---

## Authentication

- The API uses JWT-based authentication.
- Protected endpoints require a valid JWT token in the `Authorization` header:
  ```
  Authorization: Bearer <your_token>
  ```

