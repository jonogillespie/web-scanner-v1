FROM postman/newman as BUILD

ARG ENVIRONMENT

COPY ["Scripts", "Scripts"]
COPY ["Environments", "Environments"]

EXPOSE 5000

RUN newman run Scripts/postman.json \
                --environment Environments/$ENVIRONMENT.json \
                --reporters cli, junit
               
               
