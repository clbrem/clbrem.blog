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
Data factory doesn't have great support for python.
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
<li class="fragment"> Scala/Spark 👎</li>
<li class="fragment"> Python (Pandas) 🐍</li>
<li class="fragment"> <a href="https://duckdb.org/">DuckDB</a> 🦆</li>

<li class="fragment"> <a href="https://github.com/aloneguid/parquet-dotnet">Parquet.NET 👍</a>
<ul><li><a href="https://github.com/aloneguid">(Ivan G)</a></li></ul>
</li>
</ul>

</section>
<section>

## Parquet Structure

</section>
