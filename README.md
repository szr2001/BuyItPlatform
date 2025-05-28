# BuyIt (Work In Progress)

BuyIt is a full-stack online marketplace where users can post both **Sell Listings** and **Buy Listings**. Users can sell items such as cars, clothes, and random goods, while also posting what they are looking to buy. The platform ensures transparency through user ratings, and a robust commenting system, while displaying listings on a table for a game-like medieval shop feeling.

![DayBuddy Overview](gitOverview.gif)

## Features

### User Features
- **Buy/Sell Listings**:
  - Users can post items they want to sell, including images, descriptions, and prices.
  - Listings appear on the user profile on the table for a game-like display.
  - Listings can be categorized by item type for better discoverability.
  - Users can post items they are looking to buy, allowing sellers or Users to reach out if they have matching items.
- **User Interaction**:
  - Users can comment on listings to ask questions or negotiate.
  - Rate other users based on transaction experiences to build a trust system.
  - Easily Copy user phone number for contact.
- **Listings Browser**:
  - Advanced filtering for buy/sell listings based on item type, price, location, and other attributes.

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
  - **BuyItPlatform.ListingsApi**: Handles buy/sell listings.
  - **BuyItPlatform.UserRatingApi**: Handles user ratings.

### Database
- Each microservice uses its own **PostgreSQL** database for data persistence.

## Project Structure

- **Frontend**
  - `BuyItPlatform.React`
- **Gateway**
  - `BuyItPlatform.GatewayApi`
- **Services**
  - `BuyItPlatform.AuthApi`
  - `BuyItPlatform.CommentsApi`
  - `BuyItPlatform.ListingsApi`
  - `BuyItPlatform.UserRatingApi`

## Usage

### User Workflow
1. **Sign up** and create a profile.
2. **Post Buy/Sell Listings** for items you want to sell/buy, they are displayed on the table on your profile.
4. **Browse Listings** using filters to find relevant listings, based on category, tags, sub-category, transaction type and more.
5. **Interact** by commenting on listings and rating users.

This project is meant to help me practice working with React, Microservices and JWT tokens.
