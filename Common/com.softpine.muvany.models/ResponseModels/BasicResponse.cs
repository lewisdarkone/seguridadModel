

using com.softpine.muvany.models.CustomEntities;

namespace com.softpine.muvany.models.ResponseModels
{
    public class BasicResponse
    {
        public bool Data { get; set; }
        public Metadata? Meta { get; set; }
        public string? Message { get; set; }
    }
}
