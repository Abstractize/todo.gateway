# TODO.Gateway

This project is the **API Gateway** for the TODO application. It uses **YARP (Yet Another Reverse Proxy)** to route traffic between the frontend (Angular UI) and backend microservices (Auth & Task).

---

## 🧩 Tech Stack

- [.NET 9](https://learn.microsoft.com/en-us/dotnet/)
- [YARP](https://microsoft.github.io/reverse-proxy/)
- [Angular](https://angular.io/) (served via proxy in development)
- JWT Authentication
- Docker + Kubernetes (Helm/Minikube ready)

---

## 🔄 YARP Routing

Routes are defined for:

- **Auth Service** → `/api/auth/**`
- **Task Service** → `/api/tasks/**`

They are mapped to internal clusters via the YARP in-memory provider (`AddReverseProxy().LoadFromMemory()`).

---

## 🧪 Testing

Unit and integration tests are not required here (no business logic). Most of the proxy behavior is verified through integration with downstream services.

---


## 📄 License

MIT — see `LICENSE` file.
