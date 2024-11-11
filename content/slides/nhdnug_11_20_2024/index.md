---
title: Why is all of my data in Parquet?
layout: layouts/slide
author: Chris Bremer
date: 2024-05-27
tags: ["data engineering", "data lake", "parquet"]
---

<section>

### Int. Data Eng's (**DE**) office
<div class="dialog fragment">
<p class="setting ">
  Chris (<b>CB</b>) enters. It looks like he want to ask a question.
</p>
</div>


</section>
<section>
<div class="dialog">
<div class="speaker"> CB </div> <div class="line"> Hey, I'm trying to run some sanity checks on our data, but I can't find it in Azure SQL. </div>
</div>
<div class="fragment dialog">
<div class="speaker"> DE </div> <div class="line">We migrated everything to the Data Lake.</div>
</div>
<div class="fragment dialog">
<div class="speaker">DE </div> <div class="line">It's mostly in a data format called <strong>Parquet</strong>.</div>
</div>
</section>
<section>
<div class="dialog">
<div class="speaker">DE </div> <div class="line">You'll have to rewrite your query in Python.</div>
</div>
<div class="fragment dialog">
<div class="speaker">DE </div> <div class="line">It only works if you run it from a notebook in the browser IDE</div>
</div>
<div class="fragment dialog">
<div class="speaker">DE </div> <div class="line">You can't do anything until you spin up some compute. That should take 5-10 minutes.</div>
</div>

</section>

<section>

# Why the #%$@ is all my data in Parquet?
<h4 class="fragment">(A deep dive into the data lake) </h4>

</section>

<section>

## What is a data lake?
<div class="r-stack">
     <div class="fragment fade-out">
     {% image "lake_dragon.jpg", "Lake Conroe dragon", "500vw"
     %}
     </div>
       <div class="fragment grid-3by1 current-visible">
       <div class="grid-item" >
                    <p>Databricks</p>
               {% image "databricks.png", "DataBricks", "50w" %}
       </div>
         <div class="grid-item" >
          <p> Snowflake </p>
               {% image "snowflake.png", "Snowflake", "50w" %}
       </div>
       <div class="grid-item">
          <p> Cloudera <p>
               {% image "cloudera.png", "Cloudera", "50w" %}
       </div>
     </div>
     <div class="fragment">
     <ul>
       <li class="fragment">
          OLAP (index optimized reads)
       </li>
       <li class="fragment">
          Format Agnostic
       </li>
       <li class="fragment">
          Data lifecycle management
       </li>
     </ul>
     </div>
</div>
</section>

<section>

## Delta Lake
(e.g. Databricks)

```file
/delta_lake
  /_delta_log # all recent transactions
    00.json   # plus schema info
    01.json
    ...

  file1.parquet # transaction log periodically
  file2.parquet # written to parquet
```
</section>

<section>

### Int. <b>CB</b>'s office
</section>
<section>
<div class="dialog">
  <div class="setting"> 6 months ago </div>
  <div class="speaker"> DE </div> <div class="line">
Azure Data factory doesn't have great support for python.
Can we move part of our ETL to Databricks?
</div>
</div>
<div class="dialog fragment">
  <div class="setting"> 4 months ago </div>
  <div class="speaker"> DE </div>
<div class="line">
We could save hours if we streamed updates to a delta table instead of bulk loads.
</div>
</div>
</section>
<section>
<div class="dialog">
  <div class="setting"> 2 months ago </div>
  <div class="speaker"> DE </div> <div class="line"> Our data lake is using 80% fewer DTUs than traditional SQL. </div>
</div>
<div class="dialog fragment">
  <div class="speaker"> CB</div> <div class="line">Why don't we just move everything to Databricks?</div>
</div>
</section>

<section>

# How to speak Parquet

</section>
<section>

## Parquet readers
<ul>
<li class="fragment"> Scala/Spark </li>
<li class="fragment"> Python (Pandas) 🐍</li>
<li class="fragment"> <a href="https://duckdb.org/">DuckDB</a> 🦆</li>

<li class="fragment"> <a href="https://github.com/aloneguid/parquet-dotnet">Parquet.NET 👍</a>
<ul><li><a href="https://github.com/aloneguid">(Ivan G)</a></li></ul>
</li>
</ul>

</section>
<section>

### Parquet is open source!
<div class="grid-2by1">
<div class="grid-item">{% image "parquet.png", "Parquet Logo", "50w" %}</div>
<div class="grid-item">{% image "asf_logo.svg", "Apache Logo", "50w" %}</div>
</div>

* [parquet.apache.org](https://parquet.apache.org/)
* [github.com/apache/parquet-format](https://github.com/apache/parquet-format)
* [github.com/apache/parquet-java](https://github.com/apache/parquet-java)

</section>
<section>

## Questions

* How is Parquet space efficient (storage & IO)?
* Can Parquet support complex data structures?
* How do you query Parquet files?


</section>
<section>

## Parquet features
<div class="fragment">

### OLAP (NOT OLTP)
</div>

<ul>
<li class="fragment">Columnar storage</li>
<li class="fragment">Schema-on-read (vs. schema-on-write)</li>
<li class="fragment">Encodings</li>
<li class="fragment">Compression</li>
<li class="fragment">Index (?!)</li>
</ul>
</section>
<section>

## Parquet Structure

<div class="mermaid">
<pre>
  %%{init: {'theme':'dark'}}%%
	block-beta
	columns 1
	block:group1
     columns 1
      par["PAR1"]
	  c11["Column 1 (Chunk 1)"]
      c21["Column 2 (Chunk 1)"]
      space
      c21 --> cn1
	  cn1["Column n (Chunk 1)"]
	  blockarrowId6<["&nbsp;&nbsp;"]>(down)
	  c1m["Column 1 (Chunk m)"]
	  space
	  c1m --> cnm
	  cnm["Column n (Chunk m)"]
	  meta["File Metadata"]
      par2["PAR1"]
    end
</pre>
</div>
</section>
<section>

## Parquet Structure
### Column Chunks
<div class="mermaid">
<pre>
  %%{init: {'theme':'dark'}}%%
	block-beta
	columns 3
	block:group1
     columns 1
      par["PAR1"]
	  c11["Column 1 (Chunk 1)"]
      c21["Column 2 (Chunk 1)"]
      space
      c21 --> cn1
	  cn1["Column n (Chunk 1)"]
	  blockarrowId6<["&nbsp;&nbsp;"]>(down)
	  c1m["Column 1 (Chunk m)"]
	  space
	  c1m --> cnm
	  cnm["Column n (Chunk m)"]
	  meta["File Metadata"]
      par2["PAR1"]
    end
    space
    block:group2
    columns 1
      header0["Header"]
      page0["Page 0"]
      blockarrowId2<["&nbsp;&nbsp;"]>(down)
      headerk["Header"]
      pagek["Page k"]
    end
    group2 --> c11
</pre>
</div>
</section>
<section>

## Parquet Structure
### Metadata
<div class="mermaid">
<pre>
  %%{init: {'theme':'dark'}}%%
	block-beta
	columns 3
    block:group2
    columns 1
      version
      schema
      c11m["Column 1 (Chunk 1) Metadata"]
      c21m["Column 2 (Chunk 1) Metadata"]
      blockarrowId4<["&nbsp;&nbsp;"]>(down)
      len["Footer Length"]
    end
    space
	block:group1
     columns 1
      par["PAR1"]
	  c11["Column 1 (Chunk 1)"]
      c21["Column 2 (Chunk 1)"]
      space
      c21 --> cn1
	  cn1["Column n (Chunk 1)"]
	  blockarrowId6<["&nbsp;&nbsp;"]>(down)
	  c1m["Column 1 (Chunk m)"]
	  space
	  c1m --> cnm
	  cnm["Column n (Chunk m)"]
	  meta["File Metadata"]
      par2["PAR1"]
    end
    group2 --> meta
    c11m --o c11
    c21m --o c21
</pre>
</div>
</section>

<section>

## Encodings
### Base types
```thrift
  - BOOLEAN: 1 bit boolean
  - INT32: 32 bit signed ints
  - INT64: 64 bit signed ints
  - INT96: 96 bit signed ints
  - FLOAT: IEEE 32-bit floating point values
  - DOUBLE: IEEE 64-bit floating point values
  - BYTE_ARRAY: arbitrarily long byte arrays
  - FIXED_LEN_BYTE_ARRAY: fixed length byte arrays
```

</section>
<section>

## Encodings
### Logical types
```thrift
  - STRING: UTF8 ENCODED BYTE_ARRAY
  - DECIMAL:
	  INT32 or INT64 or FIXED_LEN_BYTE_ARRAY or BYTE_ARRAY
	   & PRECISION INT32 & SCALE INT32
  - DATE: INT32
  - JSON: UTF8 ENCODED BYTE_ARRAY
  - LIST (SEE NESTED TYPES)
  - MAP
  - RECORD
  - ETC.
```
</section>
<section>

## Nested Types

<div class="fragment">

Nested Lists
```thrift
Column
- [[1],[2],[3]]]
- [[4,5]]
- [[6,7],[8]]
```

</div>
<div class="fragment">

Repetition Level

```thrift
VALUES:
- 1,2,3,4,5,6,7,8
REPETITION_LEVELS:
- 0,1,1,0,2,0,2,1

```
</div>
<div class="fragment">

Encoded

```thrift
1234567801102021
```

</div>
</section>
<section>

## Encodings


```thrift
   Data: 100, 100, 100, 101, 101, 102, 103, 103
```
<div class="r-stack">
<div class="fragment fade-in-then-out wide">

Run Length Encoding

```thrift
	3, 100, 2, 101, 1, 102, 2, 103
```

</div>
	<div class="fragment fade-in-then-out wide">
	Dictionary Encoding

```thrift
- DICTIONARY: 100,101,102,103
- DATA: 0, 0, 0, 1, 1, 2, 3
```

  </div>

  <div class="fragment fade-in-then-out wide">
	Delta Encoding

```thrift
- format: [count] [first_value] [minimum_delta] [values]
- 8, 100, 0, 0,0,0,1,0,1,1,0
```

  </div>

</div>
</section>
<section>

## Compression

* SNAPPY
* GZIP
* LZO
* BROTLI
* ZSTD

</section>
<section>

## Post Credits
<div class="fragment">
{% image "no-bloomfilter-4-u.png", "No BLoom Filter 4 U!", "50w" %}
</div>

</section>




