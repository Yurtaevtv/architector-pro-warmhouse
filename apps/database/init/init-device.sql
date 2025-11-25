\c device


CREATE TABLE device_types (
    id INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE commands (
    id INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    name VARCHAR(100) NOT NULL
);

CREATE TABLE metrics (
    id INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY ,
    name VARCHAR(100) NOT NULL    
);

CREATE TABLE devices (
    id INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    device_type_id int NOT NULL REFERENCES device_types(id),
    name VARCHAR(100) NOT NULL,
    device_url VARCHAR(255)
);

CREATE TABLE device_commands(
    id INTEGER PRIMARY KEY GENERATED ALWAYS AS IDENTITY ,
    device_id int NOT NULL REFERENCES devices(id),
    command_id int NOT NULL REFERENCES commands(id)
);

CREATE TABLE device_metrics(
    device_id int NOT NULL REFERENCES devices(id),
    metric_id int NOT NULL REFERENCES metrics(id),
    value varchar(20)
);

INSERT INTO metrics(name) VALUES('on-off'), ('state'), ('temperature'), ('humidity');
INSERT INTO device_types(name) VALUES('light'), ( 'sensor'), ( 'warmer');
INSERT INTO commands(name) VALUES('on'), ( 'off'), ( 'change-state');
