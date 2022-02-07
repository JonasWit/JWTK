#FROM node:16 as build-stage
#WORKDIR /app
#COPY package*.json ./
#RUN npm install
#COPY ./ .
#RUN npm run build
#
#FROM nginx as production-stage
#EXPOSE 3000
#RUN mkdir /app
#COPY nginx.conf /etc/nginx/conf.d/default.conf
#COPY --from=build-stage /app/dist /app

# build stage
FROM node:lts-alpine as build-stage
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]