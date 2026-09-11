using Royalties_payments.Models;
using Royalties_payments.Reader;
using Royalties_payments.Service;
using System.Text.Json;

//caminho do arquivo json
//string trackPath = args[0];
//string streamPath = args[1];

string trackPath = "Data/tracks-metadata.json";
string subscribersPath = "Data/subscribers-streams.json";

//leitura do arquivo
var trackData = TrackReader.Read(trackPath);
var subscribers = SubscriberReader.Read(subscribersPath);

//Buscando o subscriber e o stream que ele deu
var subscriber = subscribers[0];
var streaming = subscriber.Streams[0];

//Relacionando o streaming com a track
TrackService trackService = new TrackService(trackData);

var track = trackService.GetTrackById(streaming.TrackId);

if (track is null)
{
    throw new InvalidDataException("Não foi encontrado nenhuma informação de track.");
}

var composition = trackService.GetComposition(track);

var composers = trackService.GetComposers(composition);

var performers = trackService.GetPerformers(track);
