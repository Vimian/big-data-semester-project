#Dockerfile to build the image for the nodejs dataset loader
FROM node:21.3.0-alpine3.17

# Create app directory
WORKDIR /usr/src/app

# Copy the datasets
COPY ./../../dataset/datasets/BitcoinTweets/ ./datasets/BitcoinTweets/
COPY ./../../dataset/datasets/Gold/export/train/m5.csv ./datasets/Gold/export/train/m5.csv
COPY ./../../dataset/datasets/Stock/ ./datasets/Stock/

# Remove all none csv files from stock dataset
RUN find ./datasets/Stock/ -type f ! -name '*.csv' -delete

# Install app dependencies
COPY ./mockup/dataset-loader/package*.json ./

# Install dependencies
RUN npm install

# Bundle app source
COPY ./mockup/dataset-loader/ .

# Run the app
CMD [ "npm", "start" ]