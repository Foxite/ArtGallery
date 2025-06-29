using ArtGallery.Domain;

namespace ArtGallery.Data;

public interface IArtRepository {
	Task<ICollection<ArtItem>> GetAllArtItems(DateOnly? min, DateOnly? max);
	Task<ICollection<ArtItem>?> GetAllArtItems(string artistName);
}

public class TestArtRepository : IArtRepository {
	private List<Artist> _artists = new() {
		new Artist() {
			Name = "Impareon",
			ArtItems = new List<ArtItem>() {
				new() {
					Date = new DateOnly(2025, 04, 21),
					Title = "My Birthday",
					Description = 
						"""
						today you bORN!!! congration!!!¡!!
						tomorrow,,,,,,,,,, deltarune
						""",
					Url = "https://cdn.bsky.app/img/feed_fullsize/plain/did:plc:rrjgaoeocfhkbub5iam6c6nu/bafkreihnar75fipezqawylnozddtivcfsoauuzacydpgs3fmy5caepieaa@jpeg",
				},
				new() {
					Date = new DateOnly(2024, 06, 25),
					Title = "Gay eepy cuddle",
					Url = "https://cdn.bsky.app/img/feed_fullsize/plain/did:plc:rrjgaoeocfhkbub5iam6c6nu/bafkreighxjeb3n3654zzo5sgxs3g5hcaxj7yu6iqs6rtnfdl3q6wgq5txi@jpeg",
				},
				new() {
					Date = new DateOnly(2025, 04, 21),
					Title = "Foxsei",
					Description = "Happy (late) birth of days foxite!",
					Url = "https://cdn.bsky.app/img/feed_fullsize/plain/did:plc:rrjgaoeocfhkbub5iam6c6nu/bafkreihocuktn7wkdvezizxah6667xri2hltademtus2skzf52oym3bdvu@jpeg",
				},
			},
		},
		new Artist() {
			Name = "AlsoAngle",
			ArtItems = new List<ArtItem>() {
				new() {
					Date = new DateOnly(2024, 11, 01),
					Title = "weezer",
					Description = "Weezer featuring Foxite, Lena, Thyrron, and Tyz",
					Url = "https://cdn.bsky.app/img/feed_fullsize/plain/did:plc:rrjgaoeocfhkbub5iam6c6nu/bafkreiex242q2wz46nyklebri6iuarpsw4knkqqveu4mgaldahapgqcssi@jpeg",
				},
				new() {
					Date = new DateOnly(2025, 10, 09),
					Title = "Ralsei hug",
					Url = "https://cdn.bsky.app/img/feed_fullsize/plain/did:plc:rrjgaoeocfhkbub5iam6c6nu/bafkreicg43jjezvflva5pcj5zkayqjxcuo4jef2zdceforwjpw3wyl2s34@jpeg",
				},
				new() {
					Date = new DateOnly(2024, 10, 17),
					Title = "Tim",
					Description = "I'm on a dinner date what do I say he's so cute and I'm nervous",
					Url = "https://cdn.bsky.app/img/feed_fullsize/plain/did:plc:rrjgaoeocfhkbub5iam6c6nu/bafkreiflijwv5ye435wbkzjymcqhjbduodu66n4tiqzuryfnmdq4n2o2ha@jpeg",
				},
			},
		},
	};

	public TestArtRepository() {
		foreach (Artist artist in _artists) {
			foreach (ArtItem item in artist.ArtItems) {
				item.Artist = artist;
			}
		}
	}

	public Task<ICollection<ArtItem>> GetAllArtItems(DateOnly? min, DateOnly? max) {
		IEnumerable<ArtItem> result = _artists.SelectMany(artist => artist.ArtItems);
		if (min.HasValue) {
			result = result.Where(item => item.Date > min.Value);
		}
		if (max.HasValue) {
			result = result.Where(item => item.Date < max.Value);
		}

		return Task.FromResult<ICollection<ArtItem>>(result.ToList());
	}
	
	public Task<ICollection<ArtItem>?> GetAllArtItems(string artistName) {
		Artist? artist = _artists.Find(artist => artist.Name == artistName);
		if (artist == null) {
			return Task.FromResult<ICollection<ArtItem>?>(null);
		}

		return Task.FromResult<ICollection<ArtItem>?>(artist.ArtItems);
	}
}
