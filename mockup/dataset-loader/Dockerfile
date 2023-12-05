#Dockerfile to build the image for the nodejs dataset loader
FROM node:21.3.0-alpine3.17

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
COPY package*.json ./

# Install dependencies
RUN npm install

# Bundle app source
COPY . .

# Run the app
CMD [ "npm", "start" ]