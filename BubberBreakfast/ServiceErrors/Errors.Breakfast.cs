using ErrorOr;

namespace BubberBreakfast.ServiceErros;


public static class Errors
{

    public static class Breakfast
    {
        public static Error Notfound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "Breakfast not found");
    }

}
