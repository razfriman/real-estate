FROM node:alpine
MAINTAINER raz.friman@razfriman.com

COPY package.json /tmp/package.json
RUN cd /tmp && npm install --quiet
RUN mkdir -p /usr/src/app && cp -a /tmp/node_modules /usr/src/app/

WORKDIR /usr/src/app
COPY . /usr/src/app

EXPOSE 4200
ENTRYPOINT ["npm", "run", "compodoc"]