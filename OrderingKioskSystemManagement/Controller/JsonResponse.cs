using OrderingKioskSystem.Application.Product;

namespace OrderingKioskSystemManagement.Api.Controller
{
    public class JsonResponse<T>
    {
        public JsonResponse(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}