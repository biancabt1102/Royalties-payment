using Royalties_payments.Models;
using System.Text.Json;

//caminho do arquivo json
string trackPath = args[0];
string streamPath = args[1];

//deseralizando o JSON
string tracksJSON = File.ReadAllText(trackPath);
string streamsJSON = File.ReadAllText(streamPath);

var tracks = JsonSerializer.Deserialize<List<Track>>(tracksJSON);
var strams = JsonSerializer.Deserialize<List<Streaming>>(streamsJSON);