/*
  HATEOAS.Net solution contains implementations of 
    Hypermedia as the engine of application state (HATEOAS)
    based on different specifications.

 HATEOAS.Net.HAL is an implementation of HAL's Specification, and it also contains some
 extra features such as Link httpVerb(GET, POST ...) and also action parameters.

 Masoud Bahrami
 http://refactor.ir
 https://twitter.com/masodbahrami
 */

namespace HATEOAS.Net.HAL
{
    public static class LinkRelations
    {
        public static readonly string Curries= "curies";

        /// <summary>
        /// o  Relation Name: alternate
        /// o Description: Designates a substitute for the link's context.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Alternate => "alternate";
        /// <summary>
        ///  o  Relation Name: appendix
        /// o Description: Refers to an appendix.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Appendix => "appendix";
        /// <summary>
        ///  o  Relation Name: bookmark
        /// o Description: Refers to a bookmark or entry point.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Bookmark => "bookmark";
        /// <summary>
        ///   o  Relation Name: chapter
        /// o Description: Refers to a chapter in a collection of resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Chapter => "chapter";
        /// <summary>
        ///  o  Relation Name: contents
        /// o Description: Refers to a table of contents.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Contents => "contents";
        /// <summary>
        ///  o  Relation Name: copyright
        /// o Description: Refers to a copyright statement that applies to the
        /// link's context.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Copyright => "copyright";
        /// <summary>
        ///  o  Relation Name: current
        /// o Description: Refers to a resource containing the most recent item(s) in a collection of resources.
        /// o  Reference: [RFC5005]
        /// </summary>
        public static string Current => "current";
        /// <summary>
        ///   o  Relation Name: describedby
        /// o Description: Refers to a resource providing information about the link's context.
        /// o Documentation: <http://www.w3.org/TR/powder-dr/#assoc-linking>
        /// </summary>
        public static string Describedby => "describedby";
        /// <summary>
        ///  o  Relation Name: edit
        /// o Description: Refers to a resource that can be used to edit the link's context.
        /// o Reference: [RFC5023]
        /// </summary>
        public static string Edit => "edit";
        /// <summary>
        /// o  Relation Name: edit-media
        /// o Description: Refers to a resource that can be used to edit media associated with the link's context.
        /// o Reference: [RFC5023]
        /// </summary>
        public static string EditMedia => "edit-media";
        /// <summary>
        ///    o  Relation Name: enclosure
        /// o Description: Identifies a related resource that is potentially large and might require special handling.
        /// o Reference: [RFC4287]
        /// </summary>
        public static string Enclosure => "enclosure";
        /// <summary>
        ///   o  Relation Name: first
        /// o Description: An IRI that refers to the furthest preceding resource in a series of resources.
        /// o  Reference: [RFC5988]
        /// o  Notes: this relation type registration did not indicate a reference.  Originally requested by Mark Nottingham in December 2004.
        /// </summary>
        public static string First => "first";
        /// <summary>
        ///    o  Relation Name: glossary
        /// o Description: Refers to a glossary of terms.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Glossary => "glossary";
        /// <summary>
        ///    o  Relation Name: help
        ///   o Description: Refers to a resource offering help(more information, links to other sources information, etc.)
        ///   o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Help => "help";
        /// <summary>
        ///    o  Relation Name: hub
        /// o Description: Refers to a hub that enables registration for notification of updates to the context.
        /// o  Reference: <http://pubsubhubbub.googlecode.com/> <http://pubsubhubbub.googlecode.com/svn/trunk/pubsubhubbub-core-0.3.html>
        /// o Notes: this relation type was requested by Brett Slatkin.
        /// </summary>
        public static string Hub => "hub";
        /// <summary>
        ///  o  Relation Name: index
        /// o Description: Refers to an index.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Index => "index";
        /// <summary>
        /// o  Relation Name: last
        /// o Description: An IRI that refers to the furthest following resource in a series of resources.
        /// o  Reference: [RFC5988]
        /// o  Notes: this relation type registration did not indicate a reference.  Originally requested by Mark Nottingham in December2004.
        /// </summary>
        public static string Last => "last";
        /// <summary>
        /// o  Relation Name: latest-version
        /// o Description: Points to a resource containing the latest(e.g., current) version of the context.
        /// o  Reference: [RFC5829]
        /// </summary>
        public static string LatestVersion => "latest-version";
        /// <summary>
        ///  o  Relation Name: license
        /// o Description: Refers to a license associated with the link's context.
        /// o Reference: [RFC4946]
        /// </summary>
        public static string License => "license";
        /// <summary>
        ///  o  Relation Name: next
        /// o Description: Refers to the next resource in a ordered series of resources.
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Next => "next";
        /// <summary>
        ///  o  Relation Name: next-archive
        /// o Description: Refers to the immediately following archive resource.
        /// o Reference: [RFC5005]
        /// </summary>
        public static string NextArchive => "next-archive";
        /// <summary>
        ///  o  Relation Name: payment
        /// o Description: indicates a resource where payment is accepted.
        /// o Reference: [RFC5988]
        /// o Notes: this relation type registration did not indicate a
        /// reference.Requested by Joshua Kinberg and Robert Sayre.  It is meant as a general way to facilitate acts of payment, and thus
        /// this specification makes no assumptions on the type of payment or
        /// transaction protocol.  Examples may include a Web page where
        /// donations are accepted or where goods and services are available
        /// for purchase.rel= "payment" is not intended to initiate an
        /// automated transaction.In Atom documents, a link element with a
        /// rel = "payment" attribute may exist at the feed/channel level and/or
        /// the entry/item level.  For example, a rel = "payment" link at the
        /// feed/channel level may point to a "tip jar" URI, whereas an entry/
        /// item containing a book review may include a rel= "payment" link
        /// that points to the location where the book may be purchased
        /// through an online retailer.
        /// </summary>
        public static string Payment => "payment";
        /// <summary>
        ///  o  Relation Name: prev
        /// o Description: Refers to the previous resource in an ordered series
        /// of resources.Synonym for "previous".
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Prev => "prev";
        /// <summary>
        /// o Relation Name: predecessor-version
        /// o  Description: Points to a resource containing the predecessor
        /// version in the version history.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string PredecessorVersion => "predecessor-version";
        /// <summary>
        ///    o  Relation Name: previous
        /// o Description: Refers to the previous resource in an ordered series of resources.Synonym for "prev".
        /// o Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Previous => "previous";
        /// <summary>
        ///  o  Relation Name: prev-archive
        /// o Description: Refers to the immediately preceding archive resource.
        /// o Reference: [RFC5005]
        /// </summary>
        public static string PrevArchive => "prev-archive";
        /// <summary>
        ///    o  Relation Name: related
        /// o Description: Identifies a related resource.
        /// o  Reference: [RFC4287]
        /// </summary>
        public static string Related => "related";
        /// <summary>
        ///    o  Relation Name: replies
        /// o Description: Identifies a resource that is a reply to the context
        /// of the link.
        /// o  Reference: [RFC4685]
        /// </summary>
        public static string Replies => "replies";
        /// <summary>
        ///    o  Relation Name: section
        /// o Description: Refers to a section in a collection of resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Section => "section";
        /// <summary>
        /// o  Relation Name: self
        /// o Description: Conveys an identifier for the link's context.
        /// o Reference: [RFC4287]
        /// </summary>
        public static string Self => "self";
        /// <summary>
        /// o  Relation Name: service
        /// o Description: Indicates a URI that can be used to retrieve a service document.
        /// o  Reference: [RFC5023]
        /// o  Notes: When used in an Atom document, this relation type specifies
        /// Atom Publishing Protocol service documents by default.  Requested
        /// by James Snell.
        /// </summary>
        public static string Service => "service";
        /// <summary>
        /// o  Relation Name: start
        /// o Description: Refers to the first resource in a collection of
        /// resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Start => "start";
        /// <summary>
        ///    o  Relation Name: stylesheet
        /// o Description: Refers to an external style sheet.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Stylesheet => "stylesheet";
        /// <summary>
        ///    o  Relation Name: subsection
        /// o Description: Refers to a resource serving as a subsection in a
        /// collection of resources.
        /// o  Reference: [W3C.REC-html401-19991224]
        /// </summary>
        public static string Subsection => "subsection";
        /// <summary>
        ///   o  Relation Name: successor-version
        /// o Description: Points to a resource containing the successor version
        /// in the version history.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string SuccessorVersion => "successor-version";
        /// <summary>
        ///  o  Relation Name: up
        /// o Description: Refers to a parent document in a hierarchy of
        /// documents.
        /// o  Reference: [RFC5988]
        /// o  Notes: this relation type registration did not indicate a
        /// reference.  Requested by Noah Slater.
        /// </summary>
        public static string Up => "up";
        /// <summary>
        ///  o  Relation Name: version-history
        /// o Description: points to a resource containing the version history
        /// for the context.
        /// o  Reference: [RFC5829]
        /// </summary>
        public static string VersionHistory => "version-history";
        /// <summary>
        /// o  Relation Name: via
        /// o Description: Identifies a resource that is the source of the
        /// information in the link's context.
        /// o Reference: [RFC4287]
        /// </summary>
        public static string Via => "via";
        /// <summary>
        ///   o  Relation Name: working-copy
        /// o Description: Points to a working copy for this resource.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string WorkingCopy => "working-copy";
        /// <summary>
        ///   o  Relation Name: working-copy-of
        /// o Description: Points to the versioned resource from which this
        /// working copy was obtained.
        /// o Reference: [RFC5829]
        /// </summary>
        public static string WorkingCopyOf => "working-copy-of";
    }
}