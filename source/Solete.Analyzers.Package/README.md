The version 1.0.2 contains the following analyzers:

|DiagnosticId | Severity | Description |
|-------------|----------|-------------|
|SR0001       | Error    | Data contract attribute required. If a class have a base class with the data contract attribute, this class must have the data contract attribute.|
|SR0002       | Error    | Data member attribute required. If a class have a data contract attribute, all public properties must have a data member attribute. |
|SR0003       | Error    | The version.md file not exist. If the file version.md exists, check the value of Build Action is 'AdditionalFiles' in its properties.|
|SR0004       | Error    | The version.md file have not contain the title section.|
|SR0005       | Error    | The version.md file have not contain the version section.|
