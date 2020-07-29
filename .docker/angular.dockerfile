FROM node:12.2.0
RUN npm install -g yarn
WORKDIR /opt/ng 
COPY .npmrc .package.json yarn.lock ./
RUN npm install
COPY . ./ 
RUN ng build --prod

CMD [ "npm", "ng serve" ]
