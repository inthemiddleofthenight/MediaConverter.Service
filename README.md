# MediaConverter.Service
ASP.NET WebApi service for realtime convert media files
Service based on ffmpeg.exe (http://ffmpeg.org/)

For start:
1. Download ffmpeg version - Get packages & executable files (http://ffmpeg.org/download.html)
2. Copy to MediaConverter.Service folder
3. Build

/api/convert<br>
POST application/json<br>
{ <br>
  "data": "?", // byte[] with source file data <br>
  "inFormat":"wav", // input format <br>
  "inFormat":"mp3" // out format <br>
}
<br>response<br>
{<br>
  "data": "?", // byte[] with target file data <br>
  "inFormat":"mp3" // out format <br>
}
