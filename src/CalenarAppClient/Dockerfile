FROM node:18.10.0-alpine As base

ARG SERVICE_PATH="src/CalenarAppClient"
ARG PACKAGE_NAME="vite"
ARG PNPM_VERSION=6

# Install package manager
RUN --mount=type=cache,id=pnpm-store,target=/root/.pnpm-store \
    npm i --global --no-update-notifier --no-fund pnpm@${PNPM_VERSION}

# Use the node user from the image (instead of the root user)
USER node

# Get all dependencies and install
FROM base AS dependencies

WORKDIR /usr/app

# COPY --chown=node:node pnpm-lock.yaml pnpm-workspace.yaml package.json .npmrc ./
COPY --chown=node:node ${SERVICE_PATH}/package.json ./
COPY --chown=node:node ${SERVICE_PATH}/.npmrc ./
COPY --chown=node:node ${SERVICE_PATH}/pnpm-lock.yaml ./

RUN --mount=type=cache,id=pnpm-store,target=/root/.pnpm-store \
    pnpm install \
    | grep -v "cross-device link not permitted\|Falling back to copying packages from store"
#--frozen-lockfile --filter ${PACKAGE_NAME}

# Build application using all dependencies, copy necessary files
FROM dependencies AS build

WORKDIR /usr/app/
COPY --chown=node:node ${SERVICE_PATH} ./

ENV NODE_ENV production
RUN cd /usr/app && pnpm build
# RUN rm -rf node_modules src \
#     && pnpm -r exec -- rm -rf node_modules

# Use base image for correct context, get built files from build stage
# Install only production dependencies, uncomment when needed
# FROM base AS deploy

# WORKDIR /usr/app

# ENV NODE_ENV production


# COPY --chown=node:node --from=build /usr/app/dist ./dist
# RUN --mount=type=cache,id=pnpm-store,target=/root/.pnpm-store \
#     pnpm install --frozen-lockfile --filter ${PACKAGE_NAME} --prod \
#     | grep -v "cross-device link not permitted\|Falling back to copying packages from store"

# ENV EXEC_PATH=dist/main.js

# CMD node ${EXEC_PATH}