# pull official base image
FROM node

# set working directory
WORKDIR /app

# install app dependencies
COPY frontend/todo-portal/package.json .
COPY frontend/todo-portal/package-lock.json .
RUN npm install

COPY . ./

# start app
CMD ["npm", "start"]