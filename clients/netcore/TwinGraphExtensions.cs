using System;
using System.Collections.Generic;
using System.Linq;

namespace Tributech.Dsk.Api.Clients.TwinApi {

	public class RelatedTwins {
		public DigitalTwin Source { get; set; }
		public DigitalTwin Target { get; set; }
		public Relationship Relationship { get; set; }
	}

	public static class TwinGraphExtensions {

		/// <summary>
		/// Get all twins with the specified model ID.
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="mdId"></param>
		/// <returns></returns>
		public static IEnumerable<DigitalTwin> GetAllTwinsWithModelId(this TwinGraph graph, string mdId) {
			return graph.DigitalTwins.Where((DigitalTwin twin) => twin.Metadata.Model == mdId);
		}

		/// <summary>
		/// Get all twins that are connected to the passed twins via a relationship.
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="twin"></param>
		/// <returns></returns>
		public static IEnumerable<DigitalTwin> GetRelatedTwins(this TwinGraph graph, DigitalTwin twin) {
			var relationships = graph.GetRelationshipsForTwin(twin);

			List<DigitalTwin> relatedTwins = new();
			foreach (var relationship in relationships)
				relatedTwins.Add(graph.GetTwinWithDtId(relationship.TargetId));

			return relatedTwins;
		}

		/// <summary>
		/// Get all relationships for the passed twin.
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="twin"></param>
		/// <returns></returns>
		public static IEnumerable<Relationship> GetRelationshipsForTwin(this TwinGraph graph, DigitalTwin twin) {
			return graph.Relationships.Where((Relationship relationship) => relationship.SourceId == twin.DtId);
		}

		/// <summary>
		/// Get the twin with the passed DtId. This will throw an exception, if the no twin is found.
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="dtId"></param>
		/// <returns></returns>
		public static DigitalTwin GetTwinWithDtId(this TwinGraph graph, Guid dtId) {
			return graph.DigitalTwins.First((DigitalTwin twin) => twin.DtId == dtId);
		}

		/// <summary>
		/// Gets both twins that are connected by a relationship.
		/// </summary>
		/// <param name="graph"></param>
		/// <param name="relationship"></param>
		/// <returns></returns>
		public static RelatedTwins GetRelatedTwins(this TwinGraph graph, Relationship relationship) {
			return new RelatedTwins {
				Relationship = relationship,
				Source = graph.GetTwinWithDtId(relationship.SourceId),
				Target = graph.GetTwinWithDtId(relationship.TargetId)
			};
		}

		/// <summary>
		/// Extract the ValueMetadataId from a stream twin. This will throw an exception, if the passed twin is not a stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static Guid GetIDFromStream(DigitalTwin stream) {
			return Guid.Parse(stream.AdditionalProperties["ValueMetadataId"] as string);
		}
	}
}
