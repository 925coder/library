# About Esent

Esent is fundamentally a low level ISAM storage engine. The idea behind tiqkdb is to use this engine to create a 'ti'ny and 'quick' document database or object oriented database. This document contains notes about the Esent storage engine itself as start looking at it.

## Instances
An instance is akin to a sql server instance, i.e. its a 'instance' of the database engine. A process typically has a single instance, but it can have more. The maximum number of instances that a process can have is determined by the JET_paramMaxInstances, which can be configured by a call to JetSetSystemParameter. The default is 64.
An instance is also the unit of recoverability for the database engine. It manages all the recovery logs and checkpoint file associated with the databases running within the instance. 

The typical sequence of operation to create and initialise an Instance are -

1. Create Instance.
2. Set system parameters.
3. Start the instance by calling JetInit
4. Terminate an instance by calling JetTerm

## Sessions
>Sessions control all access to the database and are used to control the scope of transactions. The session can be used to begin, >commit, or abort transactions. The session is also used to attach, create, or open a database. The session is used as the >context for all DDL and DML operations. To increase concurrency and parallel access to the database, multiple sessions can be begun.

A session is like an IDbConnection. There can be only a single session in a thread at a given time.


## Transactions

## Databases
An ese database is a single file. It contains the schema, indexes and records. Conventially named with a .edb extension.

## Files
There are multiple files associated with any ese database. 

- Current Log
- Pre-init Log
- Rotated Logs
- Checkpoint files
- Temporary databases
- Reserved transaction log files
- Database files


