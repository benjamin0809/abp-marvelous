namespace TalentMatrix.Org
{
    internal class BinaryObject
    {
        public BinaryObject()
        {
        }

        public int? TenantId { get; set; }
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}