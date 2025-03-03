from locust import HttpUser, task

class GPlayAuth(HttpUser):
    @task
    def registerUser(self):
        self.client.post("auth/register", json={
            "firstname":"Mehedi",
            "lastname":"Hasan",
            "mobile":"5554443333",
            "dateofbirth":"1998-09-25",
            "email":"mehedi.sun@gmail.com",
            "password": "2342343"
        }, verify=False)

 