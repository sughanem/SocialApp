# [SocialApp] (#socialappnet.herokuapp.com)

 Social networking app in which a user can create an account, log in, write a post and share it with his friends.

- What was your motivation?
- Why did you build this project? (Note: the answer is not "Because it was a homework assignment.")
- What problem does it solve?
- What did you learn?

## Table of Contents 

- [Features](#Features)
- [License](#license)


## Features

* Full Docker integration (Docker based)
* Docker Swarm Mode deployment
* Docker Compose integration and optimization for local development
* Production ready Python web server using Nginx and uWSGI
* Python Flask backend with: 

    * Flask-apispec: Swagger live documentation generation
    * Marshmallow: model and data serialization (convert model objects to JSON)
    * Webargs: parse, validate and document inputs to the endpoint / route
    * Secure password hashing by default
    * JWT token authentication
    * SQLAlchemy models (independent of Flask extensions, so they can be used with Celery workers directly)
    * Basic starting models for users and groups (modify and remove as you need)
    * Alembic migrations
    * CORS (Cross Origin Resource Sharing)

* Angular frontend:

    * Generated with Vue CLI
    * JWT Authentication handling
    * Login view
    * After login, main dashboard view
    * Vuex
    * Vue-router
    * Vuetify for beautiful material design components
    * TypeScript
    * Docker server based on Nginx (configured to play nicely with Vue-router)
    * Docker multi-stage building, so you don't need to save or commit compiled code
    * Frontend tests ran at build time (can be disabled too)
    * Made as modular as possible, so it works out of the box, but you can re-generate with Vue CLI or create it as you need, and re-use what you want


## License
This project is licensed under the terms of the MIT license.





