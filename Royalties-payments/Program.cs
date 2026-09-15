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

//Buscando as informações dos arquivos json
var subscriber = subscribers[0];
var streaming = subscriber.Streams[0];
var trackIds = subscriber.Streams.Select(s => s.TrackId).ToList();

TrackService trackService = new TrackService(trackData);
var trackInformation = trackService.GetTrackInformation(trackIds);
//
var royaltyCalculator = new RoyaltyCalculator(trackInformation, subscriber);
royaltyCalculator.Calculator();