### Multi Stage Build ###

### Estágio 1 - Obter o source e gerar o build ###

FROM node:latest AS ng-builder
RUN mkdir -p /app
WORKDIR /app
COPY package.json /app
RUN npm install
COPY . /app
RUN $(npm bin)/ng build --prod

### Estágio 2 - Subir o source para o servidor NGINX com a app Angular ###
FROM nginx
COPY nginx.conf /etc/nginx/nginx.conf

COPY --from=ng-builder /app/dist /usr/share/nginx/html
EXPOSE 80




