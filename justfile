build:
    mkdir -p bin/lsp
    dotnet build src/AvaloniaLSP/AvaloniaLanguageServer --output bin/lsp

    mkdir -p bin/solution-parser
    dotnet build src/SolutionParser/SolutionParser.csproj --output bin/solution-parser

    mkdir -p bin/xaml-styler
    dotnet build src/XamlStyler/src/XamlStyler.Console/XamlStyler.Console.csproj --output bin/xaml-styler

    mkdir -p bin/avalonia-preview
    dotnet build src/AvaloniaPreview --output bin/avalonia-preview


install:
    just build
    mkdir -p ~/.local/share/avalonia-ls
    cp bin/* ~/.local/share/avalonia-ls -r
    echo -e "#!/bin/bash\n exec ~/.local/share/avalonia-ls/xaml-styler/xstyler \"\$@\"" > ~/.local/bin/xaml-styler
    chmod +x ~/.local/bin/xaml-styler
    
    echo -e "#!/bin/bash\n exec ~/.local/share/avalonia-ls/lsp/AvaloniaLanguageServer \"\$@\"" > ~/.local/bin/avalonia-ls
    chmod +x ~/.local/bin/avalonia-ls
    
    echo -e "#!/bin/bash\n exec ~/.local/share/avalonia-ls/solution-parser/SolutionParser \"\$@\"" > ~/.local/bin/avalonia-solution-parser
    chmod +x ~/.local/bin/avalonia-solution-parser

    echo -e "#/!bin/bash\n exec ~/.local/share/avalonia-ls/avalonia-preview/AvaloniaPreview \"\$@\"" > ~/.local/bin/avalonia-preview
    chmod +x ~/.local/bin/avalonia-preview

    @echo "INSTALLATION COMPLETE!"

    
uninstall:
    rm -rf ~/.local/share/avalonia-ls
    rm ~/.local/bin/xaml-styler ~/.local/bin/avalonia-ls ~/.local/bin/avalonia-solution-parser ~/.local/bin/avalonia-preview
    echo "UNINSTALLATION COMPLETE"
    
