---
title: Why is all of my data in Parquet?
layout: layouts/slide
author: Chris Bremer
date: 2024-05-27
tags:

---

<!-- Any section element inside of this page is displayed as a slide -->
<section>

### Data Engineer's (**DE**) office 
<p class="fragment text-italic">
  Chris (<b>CB</b>) enters. It looks like he want to ask a question.
</p>
</section>
<section>
<ul class="dialog">
<li> CB: Hey, I'm trying to run some sanity checks on our data, but I can't find it in Azure SQL.
<li class="fragment"> DE: We migrated everything to the Data Lake.</li>
<li class="fragment">DE: It's mostly in a data format called <strong>Parquet</strong>.</li>
<li class="fragment" > 
     DE: You'll have to rewrite your query in Python. 
</li>
<li class="fragment" > 
     DE: It only works if you run it from a notebook in the browser IDE.
</li>
<li class="fragment"> 
     DE: You can't do anything until you spin up some compute. That should take 5-10 minutes.
</li>
</ul>

</section>

<section>

## Why the #%$@ is all my data in Parquet?
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

