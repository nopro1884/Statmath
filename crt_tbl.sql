CREATE TABLE "Plans" (
  "Id" UUID PRIMARY KEY,
  "Machine" VARCHAR(10) NOT NULL,
  "Job" INT NOT NULL,
  "StartedAt" TIMESTAMP(0),
  "EndedAt" TIMESTAMP(0)
);