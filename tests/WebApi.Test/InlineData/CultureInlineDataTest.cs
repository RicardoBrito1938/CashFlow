using System.Collections;

namespace WebApi.Test.InlineData;

public class CultureInlineDataTest: IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return ["pt-BR"];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}