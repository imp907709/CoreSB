namespace CoreSB.Universal
{

    public interface IValidatorCustom
    {
        bool isValid<T>(T instance);
    }
}
