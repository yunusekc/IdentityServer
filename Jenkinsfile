pipeline {
    agent any
    environment {
        APP_NAME = "identityserver" 
        IMAGE_TAG = "latest"
    }

    stages {        
        stage('Build Image') {
            steps {
                script {
                    echo "Docker Image oluşturuluyor..."
                    sh "docker build -f IdentityServer/Dockerfile -t ${APP_NAME}:${IMAGE_TAG} ."
                }
            }
        }

        stage('Deploy') {
            steps {
                script {
                    echo "Eski container temizleniyor ve yenisi başlatılıyor..."
                    sh "docker rm -f ${APP_NAME} || true"
                    sh "docker run -d -p 5000:80 --name ${APP_NAME} ${APP_NAME}:${IMAGE_TAG}"
                }
            }
        }
    }
    
    post {
        success {
            echo "Başarıyla canlıya alındı."
        }
        failure {
            echo "Bir şeyler ters gitti."
        }
    }
}