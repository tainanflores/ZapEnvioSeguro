#!/bin/bash

# Exibir mensagem de ajuda se os parâmetros não forem fornecidos
if [ "$#" -ne 3 ]; then
    echo "Uso: $0 <versão> <branch> <comentário>"
    exit 1
fi

# Atribuir argumentos a variáveis
VERSION=$1
BRANCH=$2
COMMENT=$3

# Adicionar mudanças a o Git
echo "Adicionando mudanças ao Git..."
git add .

# Criar uma tag anotada
echo "Criando tag $VERSION..."
git tag -a "$VERSION" -m "Release $VERSION"

# Fazer commit com a mensagem fornecida
echo "Criando commit..."
git commit -m "$COMMENT"

# Enviar mudanças para a branch especificada
echo "Enviando para a branch $BRANCH..."
git push origin "$BRANCH"

# Enviar a tag para o repositório remoto
echo "Enviando a tag $VERSION..."
git push origin "$VERSION"


echo "Pressione Enter para sair..."
read
