## Netcore 2.2 with GraphQL and EntityFrameworkCore MSSQL
---
This is an example project of how to use GraphQL with EF to query and manipulate with data.

EF will populate person table with 3 entries.

---
Get started:


Modify your sql server connection string in the `appsettings.Development.json` file if needed.

```
dotnet restore

dotnet build

dotnet run
```

### Playground

Open [https://localhost:5001](https://localhost:5001) in your browser.

- *Query* all persons
```
{
  allPersons {
    id
    name
    
  }
}
```

- *Query* person with a name `"Rachelle"`
```
{
  allPersons(name: "Rachelle") {
    id
    name
    
  }
}
```

- *Mutation* add person

```
mutation PersonMutation($person: PersonInputType!){
  addPerson(person: $person){
    id
  }
}
```
