### Product Endpoints

- **Get Products**:

  - Method: GET
  - URL: `http://catalog.api:5010/api/products`
- **Create Product**:

  - Method: POST
  - URL: `http://catalog.api:5010/api/products`
- **Get Product by ID**:

  - Method: GET
  - URL: `http://catalog.api:5010/api/products/{id}`
- **Update Product by ID**:

  - Method: PUT
  - URL: `http://catalog.api:5010/api/products/{id}`
- **Delete Product by ID**:

  - Method: DELETE
  - URL: `http://catalog.api:5010/api/products/{id}`

### Brand Endpoints

- **Get Brands**:

  - Method: GET
  - URL: `http://catalog.api:5010/api/brands`
- **Create Brand**:

  - Method: POST
  - URL: `http://catalog.api:5010/api/brands/create`
- **Update Brand by ID**:

  - Method: PUT
  - URL: `http://catalog.api:5010/api/brands/update/{id}`
- **Delete Brand by ID**:

  - Method: DELETE
  - URL: `http://catalog.api:5010/api/brands/delete/{id}`

### Type(Category) Endpoints

- **Get Types**:

  - Method: GET
  - URL: `http://catalog.api:5010/api/types`
- **Get Type by ID**:

  - Method: GET
  - URL: `http://catalog.api:5010/api/types/{id}`
- **Create Type**:

  - Method: POST
  - URL: `http://catalog.api:5010/api/types/create`
- **Update Type by ID**:

  - Method: PUT
  - URL: `http://catalog.api:5010/api/types/update/{id}`
- **Delete Type by ID**:

  - Method: DELETE
  - URL: `http://catalog.api:5010/api/types/delete/{id}`

### Shopping Cart Endpoints

- **Get Shopping Cart ID by Username**:

  - Method: GET
  - URL: `http://basket.api:80/ShoppingCartApi/GetShoppingCartId/{userName}`
- **Update Shopping Cart Asynchronously**:

  - Method: POST
  - URL: `http://basket.api:80/ShoppingCartApi/UpdateShoppingCartAsync`
