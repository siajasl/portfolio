use hex;
use lkernel::{FetcherConfig, Kernel, KernelConfig};
use ltypes::chain::BlockHash;
use ltypes::crypto::Digest;
use toml;

const MSG_DIGEST_BLAKE2B_HEX: &str =
    "44682ea86b704fb3c65cd16f84a76b621e04bbdb3746280f25cf062220e471b4";

const PATH_TO_ROOT: &str = "/Users/asladeofgreen/Coding/projects-zk/litmus-zk-sp1/script/env.toml";

fn main() {
    let digest_bytes = hex::decode(MSG_DIGEST_BLAKE2B_HEX).unwrap();

    let config = KernelConfig {
        name_of_chain: "xxx".to_string(),
        trusted_block_hash: BlockHash::new(Digest::from(digest_bytes)),
        fetcher: FetcherConfig::FileSystem {
            path_to_root: PATH_TO_ROOT.to_string(),
        },
    };

    let config_as_str = toml::to_string(&config).unwrap();
    println!("{:?}", config_as_str);
}
