FROM loadimpact/k6 as BUILD

ARG ENVIRONMENT

COPY ["scripts", "scripts"]

RUN k6 run scripts/$ENVIRONMENT.js