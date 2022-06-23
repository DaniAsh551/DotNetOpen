# IRuleContainer.AddRule&lt;TRule&gt; method

Add a rule into the container.

```csharp
public void AddRule<TRule>()
    where TRule : IRule
```

| parameter | description |
| --- | --- |
| rule | The rule to be added. |

## Exceptions

| exception | condition |
| --- | --- |
| [InvalidRuleException](../InvalidRuleException.md) | Thrown when the rule is invalid. |

## See Also

* interface [IRule](../IRule.md)
* interface [IRuleContainer](../IRuleContainer.md)
* namespace [DotNetOpen.FileService](../../DotNetOpen.FileService.Abstractions.md)

<!-- DO NOT EDIT: generated by xmldocmd for DotNetOpen.FileService.Abstractions.dll -->