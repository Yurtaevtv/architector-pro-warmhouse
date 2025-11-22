\c device


CREATE TABLE device_types (
    id int PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE commands (
    id int PRIMARY KEY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE metrics (
    id int PRIMARY KEY ,
    name VARCHAR(100) NOT NULL    
);

CREATE TABLE devices (
    id int PRIMARY KEY,
    device_type_id int NOT NULL REFERENCES device_types(id),
    name VARCHAR(100) NOT NULL,
    device_address VARCHAR(255)
);

CREATE TABLE device_commands(
    id int PRIMARY KEY,
    device_id int NOT NULL REFERENCES devices(id),
    available_commands int NOT NULL REFERENCES commands(id)
);

CREATE TABLE device_metrics(
    id int PRIMARY KEY,
    device_id int NOT NULL REFERENCES devices(id),
    available_metrics int NOT NULL REFERENCES metrics(id)
);
