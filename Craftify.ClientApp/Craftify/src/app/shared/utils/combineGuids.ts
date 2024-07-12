export function combineGuids(guid1: string, guid2: string): string {
    // Convert GUID strings to byte arrays
    const bytes1 = guidToBytes(guid1);
    const bytes2 = guidToBytes(guid2);
    
    // Combine bytes using XOR
    const result = new Uint8Array(16);
    for (let i = 0; i < 16; i++) {
        result[i] = bytes1[i] ^ bytes2[i];
    }
    
    // Convert back to GUID string
    return bytesToGuid(result);
}

// Helper function to convert GUID string to byte array
function guidToBytes(guid: string): Uint8Array {
    const hex = guid.replace(/-/g, '');
    const bytes = new Uint8Array(16);
    
    for (let i = 0; i < 16; i++) {
        bytes[i] = parseInt(hex.substr(i * 2, 2), 16);
    }
    
    return bytes;
}

// Helper function to convert byte array to GUID string
function bytesToGuid(bytes: Uint8Array): string {
    const hex = Array.from(bytes, (byte) => byte.toString(16).padStart(2, '0')).join('');
    
    return `${hex.substr(0, 8)}-${hex.substr(8, 4)}-${hex.substr(12, 4)}-${hex.substr(16, 4)}-${hex.substr(20)}`;
}