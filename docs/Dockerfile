FROM jupyter/base-notebook:latest

# Install .NET CLI dependencies

ARG NB_USER=fsdocs-user
ARG NB_UID=1000
ENV USER ${NB_USER}
ENV NB_UID ${NB_UID}
ENV HOME /home/${NB_USER}
ENV DOTNET_SDK_VERSION=6.0.428
ENV \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container
    NUGET_XMLDOC_MODE=skip

WORKDIR ${HOME}

USER root

ENV \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Enable correct mode for dotnet watch (only mode supported in a container)
    DOTNET_USE_POLLING_FILE_WATCHER=true \
    # Skip extraction of XML docs - generally not useful within an image/container - helps performance
    NUGET_XMLDOC_MODE=skip \
    # Opt out of telemetry until after we install jupyter when building the image, this prevents caching of machine id
    DOTNET_INTERACTIVE_CLI_TELEMETRY_OPTOUT=true

# Install .NET CLI dependencies
RUN apt-get update \
    && DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends \
        libc6 \
        libgcc1 \
        libgssapi-krb5-2 \
        libicu70 \
        libssl3 \
        libstdc++6 \
        zlib1g \
        curl \
        git \
    && rm -rf /var/lib/apt/lists/*

# When updating the SDK version, the sha512 value a few lines down must also be updated.
# Install .NET SDK
RUN curl -SL --output dotnet.tar.gz https://dotnetcli.azureedge.net/dotnet/Sdk/6.0.428/dotnet-sdk-6.0.428-linux-x64.tar.gz \
    && echo "Downloading .NET SDK 6.0.428..." \
    && dotnet_sha512='bb87b743f2aa78234f0caa193b809a63efc2e99b537b44a0c2d1c3f49e48a447c93f0a3d92a6e39aca17d519b0df2582123b1db65f97f8d0d75ccbe7aa5a1a84' \
    && echo "$dotnet_sha512 dotnet.tar.gz" | sha512sum -c - \
    && mkdir -p /usr/share/dotnet \
    && tar -ozxf dotnet.tar.gz -C /usr/share/dotnet \
    && rm dotnet.tar.gz \
    && ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet \
    && dotnet help

# Copy notebooks
COPY ./ ${HOME}/notebooks/

# Copy package sources
# COPY ./NuGet.config ${HOME}/nuget.config

RUN chown -R ${NB_UID} ${HOME}
USER ${USER}

# Clone and build Furnace-cpu bundle to get the latest TorchSharp and libtorch-cpu packages downloaded and cached by nuget within the Docker image
# This the makes user experience faster when running #r "nuget: Furnace-cpu
RUN git clone --depth 1 https://github.com/Furnace/Furnace.git \
    && dotnet build Furnace/bundles/Furnace-cpu

#Install nteract 
RUN pip install nteract_on_jupyter

# Install lastest build from master branch of Microsoft.DotNet.Interactive
RUN dotnet tool install -g --add-source "https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet-tools/nuget/v3/index.json" Microsoft.dotnet-interactive

#latest stable from nuget.org
#RUN dotnet tool install -g Microsoft.dotnet-interactive --add-source "https://api.nuget.org/v3/index.json"

ENV PATH="${PATH}:${HOME}/.dotnet/tools"
RUN echo "$PATH"

# Install kernel specs
RUN dotnet interactive jupyter install

# Enable telemetry once we install jupyter for the image
ENV DOTNET_INTERACTIVE_CLI_TELEMETRY_OPTOUT=false

# Set root to notebooks
WORKDIR ${HOME}/notebooks/