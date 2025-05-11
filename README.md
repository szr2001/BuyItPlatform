# BuyIt (Work In Progress)

BuyIt is a full-stack online marketplace where users can post both **Sell Listings** and **Buy Listings**. Users can sell items such as cars, clothes, and random goods, while also posting what they are looking to buy. The platform ensures transparency through user ratings, reporting, and a robust commenting system.

## Features

### User Features
- **Sell Listings**:
  - Users can post items they want to sell, including images, descriptions, and prices.
  - Listings can be categorized by item type for better discoverability.
- **Buy Listings**:
  - Users can post items they are looking to buy, allowing sellers or Users to reach out if they have matching items.
- **User Interaction**:
  - Users can comment on listings to ask questions or negotiate.
  - Reporting system to flag inappropriate listings or users.
  - Rate other users based on transaction experiences to build a trust system.
- **Listings Browser**:
  - Advanced filtering for buy/sell listings based on item type, price, location, and other attributes.
  
### Admin Features
- **Admin Panel**:
  - Manage user reports and take appropriate actions (e.g., removing fraudulent listings, banning users).
  - Moderate user ratings and comments.

## Technology Stack

### Frontend
- **React** for a modern and dynamic user interface.
- **Css** for styling.

### Backend
- **ASP.NET 8** RESTful API Gateway handling authentication and request routing.
- **Microservice Architecture** with multiple **ASP.NET 8 RESTful API** services handling different functionalities:
  - **BuyItPlatform.GatewayApi**: Manages API request routing.
  - **BuyItPlatform.AuthApi**: Handles authentication and user management.
  - **BuyItPlatform.CommentsApi**: Manages listing comments and discussions.
  - **BuyItPlatform.ListingReportingApi**: Manages reports on listings.
  - **BuyItPlatform.ListingsApi**: Handles buy/sell listings.
  - **BuyItPlatform.UserReportingApi**: Handles reports on users.

### Database
- Each microservice uses its own **PostgreSQL** database for data persistence.

## Project Structure

- **Frontend**
  - `BuyItPlatform.React`
- **Gateway**
  - `BuyItPlatform.GatewayApi`
- **Integration** (potential integrations for third-party services)
- **Services**
  - `BuyItPlatform.AuthApi`
  - `BuyItPlatform.CommentsApi`
  - `BuyItPlatform.ListingReportingApi`
  - `BuyItPlatform.ListingsApi`
  - `BuyItPlatform.UserRatingApi`
  - `BuyItPlatform.UserReportingApi`

## Usage

### User Workflow
1. **Sign up** and create a profile.
2. **Post Sell Listings** for items you want to sell.
3. **Post Buy Listings** for items you are looking for.
4. **Browse Listings** using filters to find relevant posts.
5. **Interact** by commenting on listings and rating users.
6. **Report** inappropriate listings or users if necessary.

### Admin Workflow
1. Open the **Admin Panel**.
2. Review **user reports** and take action if needed.
3. Moderate **user ratings and comments**.


This project is meant to help me practice working with React, Microservices and JWT tokens.
