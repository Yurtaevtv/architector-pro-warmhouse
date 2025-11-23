\c device


CREATE TABLE device_types (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE commands (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE metrics (
    id SERIAL PRIMARY KEY ,
    name VARCHAR(100) NOT NULL    
);

CREATE TABLE devices (
    id SERIAL PRIMARY KEY,
    device_type_id int NOT NULL REFERENCES device_types(id),
    name VARCHAR(100) NOT NULL,
    device_url VARCHAR(255)
);

CREATE TABLE device_commands(
    id SERIAL PRIMARY KEY,
    device_id int NOT NULL REFERENCES devices(id),
    command_id int NOT NULL REFERENCES commands(id)
);

CREATE TABLE device_metrics(
    id SERIAL PRIMARY KEY,
    device_id int NOT NULL REFERENCES devices(id),
    metric_id int NOT NULL REFERENCES metrics(id)
);


INSERT INTO metrics(name) VALUES('on-off'), ('state'), ('temperature'), ('humidity');
INSERT INTO device_types(name) VALUES('light'), ( 'sensor'), ( 'warmer');
INSERT INTO commands(name) VALUES('on'), ( 'off'), ( 'change-state');
