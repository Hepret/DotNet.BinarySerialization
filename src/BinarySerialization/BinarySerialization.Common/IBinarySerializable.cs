namespace BinarySerialization.Common;

public interface IBinarySerializable  
{ 
    byte[] ToBytes();
    string AsHexString(char separator = '_'); 
    string GetMetadata();
}