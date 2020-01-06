# MediaConverter.Service
ASP.NET WebApi service for realtime convert media files
Service based on ffmpeg.exe (http://ffmpeg.org/)

For start:
1. Download ffmpeg version - Get packages & executable files (http://ffmpeg.org/download.html)
2. Copy to MediaConverter.Service folder
3. Build

/api/convert
POST application/json
{
  "data": "?", // byte[] with source file data,
  "inFormat":"wav", // input format
  "inFormat":"mp3" // out format
}

response
{
  "data": "?", // byte[] with target file data,
  "inFormat":"mp3" // out format
}
