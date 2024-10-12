# LetsGetChecked-CodeChallenge
 
# Start docker Container with Localstack
```bash 
$ docker run --rm -it --name LetsGetChecked -d -p 4566:4566 -p 4510-4559:4510-4559 localstack/localstack
```
# Install AWS CLI
```bash
$ awslocal s3api create-bucket --bucket sample-bucket
$ awslocal s3api list-buckets
```