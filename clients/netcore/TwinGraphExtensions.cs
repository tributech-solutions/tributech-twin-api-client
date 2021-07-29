using System;
using System.Collections.Generic;
using System.Linq;

namespace Tributech.Dsk.TwinApi.Client {

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
		/// <param name="dtmi"></param>
		/// <returns></returns>
		public static IEnumerable<DigitalTwin> GetAllTwinsWithModelId(this TwinGraph graph, string dtmi) {
			return graph.DigitalTwins.Where((DigitalTwin twin) => twin.Metadata.Model == dtmi);
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
