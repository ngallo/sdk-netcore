# Contributing to Permguard

Thank you sincerely for considering contributing to the Permguard project. We highly value the input and engagement of our community members.

Please make sure to review our set guidelines detailed in the [conduct protocol](CODE_OF_CONDUCT.md) that governs interactions within this project's sphere.

Moreover, familiarize yourself with the guidelines listed below to increase the chances of your submissions getting approved.

> Please note that we are currently following an internal roadmap and are not accepting feature contributions from the community at the moment. However, this policy will change soon. In the meantime, we welcome bug fixes and documentation improvements.

Thank you once again for your interest and support.
To get any help or get help:

- Email us at [opensource@permguard.com](mailto:opensource@permguard.com)

## Contribution Steps

To contribute to the project, follow the steps below:

- Fork this repository.
- Clone the repository you forked.
- Create a branch with specified name. It's better to relate it with your issue title.
- Make necessary changes and commit those changes. Make sure to test your changes.
- Push changes to your branch.
- Submit your changes for review.

## Committing changes

Please help us to keep our commit messages clean and informative. Here the applied form of commit message:

```bash
<type>(optional scope): <description>
```

Examples:

```bash
  feat: add new feature
  fix: correct minor typos
  docs: update README.md
  style: remove trailing spaces
  refactor: refactor code
  test: add test cases
```

Types:

- `feat:` new feature
- `fix:` bug fix
- `refactor:` refactoring production code
- `test:` adding tests, refactoring test; no production code change
- `docs:` documentation
- `style:` formatting, missing semi colons, etc; no code change
- `build:` code change that affects the build system or external dependencies
- `chore:` updating build tasks, package manager configs, etc; no production code change
- `perf:` code change that improves performance
- `security:` code change that improves security
- `breaking:` code change that breaks existing functionality
- `ci:` updating CI build tasks, etc; no production code change
- `release:` code change for release

## Submitting pull requests

Once you wish to get started contributing to the code base, please refer to our [development guide](DEVELOPMENT.md) for a how-to.

> Please note we accept pull requests from forks only.

Before creating a pull request, please ensure that your changes are tested and that the documentation is updated accordingly.

### Pull request reccomendations

- Before submitting a pull request, please raise an issue to discuss the changes you wish to make. This will help us understand the context of your changes and provide feedback.
- Make sure sure each source file include the appropriate license header.

  ```go
  // Copyright (c) 2022 Nitro Agility S.r.l.
  // SPDX-License-Identifier: Apache-2.0
  ```

- Add test cases for your changes.
- Ensure the documentation is updated accordingly to reflect the changes you made.
- It is very important to commit only required files and not any unnecessary files, whenever necessary it is recommended to use `.gitignore` to exclude files.
- Code cannot be reverted if you by mistake commit any sensitive information, so please make sure to not commit any sensitive information.
- Do not add third-party content in-line without attribution. Use links where possible.
- Make sure the development guidance is followed.

---

# Generating Java Code from Protocol Buffers

To generate Java code from your `.proto` files, follow these steps:

1. **Download the `protoc-gen-grpc-java` Plugin:**

 The `protoc-gen-grpc-java` plugin is essential for generating gRPC Java interfaces from `.proto` files. To obtain the appropriate version for your operating system:

   - **Maven Central Repository:** Visit the [Maven Central Repository](https://repo1.maven.org/maven2/io/grpc/protoc-gen-grpc-java/) and navigate to the desired version. Within the version directory, locate and download the binary compatible with your operating system.

   Ensure that the downloaded `protoc-gen-grpc-java` binary is executable and accessible in your system's `PATH`.

2. **Add Java Options to Your `.proto` File:**

   To specify the Java package and the outer class name for the generated code, include the following options in your `.proto` file:
    
    * `option java_package = "com.permguard.pep.internal.proto";`
    * `option java_outer_classname = "AuthorizationCheck";`

   ```proto
    package policydecisionpoint;
  
    option go_package = "github.com/permguard/permguard/internal/hosts/api/pdp/v1";
    
    //HERE
    option java_package = "com.permguard.pep.internal.proto";
    option java_outer_classname = "AuthorizationCheck";
   
    // PolicyStore is the location where policies are maintained.
    message PolicyStore {
        string Kind = 1;
        string ID = 2;
    }```
  
  These options ensure that the generated Java classes are placed in the correct package and have the desired class names. 

3. **Generate Java Code Using protoc**:

    Use the `protoc` compiler along with the downloaded `protoc-gen-grpc-java` plugin to generate the Java code. Replace `/path/to/protoc-gen-grpc-java` with the actual path to the downloaded plugin:

  ```shell
  protoc \
  --plugin=protoc-gen-grpc-java=/path/to/protoc-gen-grpc-java \
  -I=src/main/proto \
  --java_out=src/main/java \
  --grpc-java_out=src/main/java \
  src/main/proto/v1/authorization_check.proto
  ```
  
  This command specifies:
  * The plugin to use for generating gRPC Java code.
  * The input directory containing your `.proto` files (`-I=src/main/proto`).
  * The output directory for the generated Java code (`--java_out=src/main/java` and `--grpc-java_out=src/main/java`).
  * The specific `.proto` file to process (`src/main/proto/v1/authorization_check.proto`).

By following these steps, you can generate the necessary Java classes from your Protocol Buffers definitions.