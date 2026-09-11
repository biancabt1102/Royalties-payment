using Royalties_payments.Models;
using Royalties_payments.Reader;
using Royalties_payments.Service;
using System.Text.Json;

//caminho do arquivo json
//string trackPath = args[0];
//string streamPath = args[1];

string trackPath = "Data/tracks-metadata.json";
string subscribersPath = "Data/subscribers-streams.json";

Console.WriteLine(Path.GetFullPath(trackPath));
Console.WriteLine(File.Exists(trackPath));

var trackData = TrackReader.Read(trackPath);
var subscribers = SubscriberReader.Read(subscribersPath);

Console.WriteLine($"Tracks: {trackData.Tracks.Count}");
Console.WriteLine($"Compositions: {trackData.Compositions.Count}");
Console.WriteLine($"Composer: {trackData.Composers.Count}");
Console.WriteLine($"Performer: {trackData.Performers.Count}");
Console.WriteLine($"Subscribers: {subscribers.Count}");

// Testando relacionamento Stream → Track
var subscriber = subscribers[0];
var streaming = subscriber.Streams[0];

var track = trackData.Tracks.FirstOrDefault(t => t.Id == streaming.TrackId);

Console.WriteLine($"Track reproduzida: {track?.Title}");